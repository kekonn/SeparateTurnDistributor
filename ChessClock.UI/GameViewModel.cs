using ChessClock.Data;
using ChessClock.Model;
using ChessClock.UI.Properties;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ChessClock.UI
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Game game;
        private readonly IGameRepository gameRepository;
        private FileSystemWatcher saveFileWatcher;
        private ILogger Logger = LogManager.GetCurrentClassLogger();

        public Player CurrentPlayer => game.CurrentPlayer;
        public IReadOnlyList<Player> Players => game.Players;
        public string Name
        {
            get
            {
                return game.Name;
            }
            set
            {
                game.Name = value;
                FirePropertyChanged(nameof(Name));
            }
        }
        public DateTimeOffset SaveGameTime => Filesystem.GetSaveFileLastWrite(game, Settings.Default.HotSeatFolder);
        public string SaveFileLocation
        {
            get
            {
                return game.SavefileName;
            }
            set
            {
                SetSaveFile(value);
            }
        }

        private DateTimeOffset lastUpdated;
        public DateTimeOffset LastUpdateCheck
        {
            get
            {
                return lastUpdated;
            }
            set
            {
                if (lastUpdated != value)
                {
                    lastUpdated = value;
                    FirePropertyChanged(nameof(LastUpdateCheck));
                }
            }
        }

        public bool IsMyTurn => game.CurrentPlayer.Equals(PlayerUtilities.LoadSystemPlayer());

        public GameViewModel(Game game, IGameRepository repository)
        {
            this.game = game;
            this.gameRepository = repository;
            this.lastUpdated = game.LastUpdated;
            SetupSavefileWatcher();
        }

        private void FirePropertyChanged(string propertyName)
        {
            var args = new PropertyChangedEventArgs(propertyName);

            PropertyChanged(this, args);
        }

        private void FirePropertiesChanged(params string[] propertyNames)
        {
            Array.ForEach(propertyNames, FirePropertyChanged);
        }

        public void NextTurn()
        {
            var validSaveFile = ValidateSaveFile();

            if (!validSaveFile)
            {
                MessageBox.Show("Pick a valid save file first", "Save file error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            game.NextTurn();
            gameRepository.Add(game);
            gameRepository.UploadSaveFile(game, Settings.Default.HotSeatFolder);

            FirePropertiesChanged(nameof(CurrentPlayer), nameof(SaveGameTime), nameof(IsMyTurn));
        }

        private void SetSaveFile(string fileName)
        {
            var isValid = ValidateSaveFile(fileName);
            if (!isValid)
            {
                MessageBox.Show("Pick a valid save file first", "Save file error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var saveFile = Path.GetFileNameWithoutExtension(fileName);

            if (saveFile != game.SavefileName)
            {
                game.SavefileName = saveFile;

                FirePropertiesChanged(nameof(SaveFileLocation));
                SetupSavefileWatcher();
            }
        }

        private void SetupSavefileWatcher()
        {
            if (saveFileWatcher != null)
            {
                saveFileWatcher.Dispose();
                saveFileWatcher = null;
            }

            var filePath = Filesystem.GetHotSeatSaveFileFullName(Settings.Default.HotSeatFolder, game.SavefileName);

            if (!File.Exists(filePath))
                return;

            saveFileWatcher = new FileSystemWatcher(Path.GetDirectoryName(filePath), "*.Civ6Save");
            saveFileWatcher.EnableRaisingEvents = true;
            saveFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            saveFileWatcher.Changed += SaveFileWatcher_Changed;
        }

        private void SaveFileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            var changeType = e.ChangeType;
            if (changeType != WatcherChangeTypes.Changed)
                return;

            var saveName = Path.GetFileNameWithoutExtension(e.FullPath);

            if (saveName != game.SavefileName)
                return;

            var fileName = e.FullPath;

            Logger.Debug($"Detected write to {fileName}");

            var baseTime = File.GetLastWriteTimeUtc(fileName);
            var lastWrite = new DateTimeOffset(baseTime, TimeZoneInfo.Local.GetUtcOffset(baseTime));

            game.LastUpdated = lastWrite;

            FirePropertyChanged(nameof(SaveGameTime));
        }

        private bool ValidateSaveFile(string fileName = null)
        {
            fileName = fileName ?? Filesystem.GetHotSeatSaveFileFullName(Settings.Default.HotSeatFolder, Filesystem.GetSaveFileName(game));
            return File.Exists(fileName);
        }

        public void Update()
        {
            Logger.Info("Checking for update");
            LastUpdateCheck = DateTimeOffset.Now;

            var hasUpdated = gameRepository.HasUpdated(game) || Filesystem.GetSaveFileLastWrite(game, Settings.Default.HotSeatFolder) < gameRepository.GetSaveGameLastModifiedTime(game);
            if (hasUpdated)
            {
                var remoteSavefileLastModified = gameRepository.GetSaveGameLastModifiedTime(game);
                var remoteFileIsNewer = SaveGameTime < remoteSavefileLastModified;

                if (remoteFileIsNewer)
                {
                    Logger.Info($"Detected newer remote save file for {game}");
                    saveFileWatcher.EnableRaisingEvents = false;

                    var fullSavefileName = Filesystem.GetHotSeatSaveFileFullName(Settings.Default.HotSeatFolder, Filesystem.GetSaveFileName(game));
                    game = gameRepository.UpdateGameAndSaveFile(game, fullSavefileName);

                    FirePropertiesChanged(nameof(SaveGameTime), nameof(CurrentPlayer), nameof(Players), nameof(Name), nameof(IsMyTurn), nameof(LastUpdateCheck));
                    saveFileWatcher.EnableRaisingEvents = true;
                }
            }
        }
    }
}
