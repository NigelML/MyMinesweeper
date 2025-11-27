using System;
public static class MyEventSystem
{
        public static Action OnGameOver;
        public static Action OnGameWin;
        public static Action OnCellChecked;
        public static Action OnStartGame;
        public static Action OnSetFlags;
        public static Action OnPauseGame;

        public static void RaiseTryGameOver()
                => OnGameOver?.Invoke();
        public static void RaiseTryGameWin()
                => OnGameWin?.Invoke();
        public static void RaiseCellChecked()
            => OnCellChecked?.Invoke();
        public static void RaiseStartGame()
            => OnStartGame?.Invoke();
        public static void RaiseSetFlags()
            => OnSetFlags?.Invoke();
        public static void RaisePauseGame()
        => OnPauseGame?.Invoke();

        public static void ClearAllEvents()
        {
                OnGameOver = null;
                OnGameWin = null;
                OnCellChecked = null;
                OnStartGame = null;
                OnSetFlags = null;
                OnPauseGame = null;
        }

}
