public static class MyEventSystem
{
        public static System.Action OnGameOver;
        public static System.Action OnGameWin;
        public static System.Action OnCellChecked;
        public static System.Action OnStartGame;

        public static void RaiseTryGameOver()
                => OnGameOver?.Invoke();

        public static void RaiseTryGameWin()
                => OnGameWin?.Invoke();
        public static void RaiseCellChecked()
            => OnCellChecked?.Invoke();
        public static void RaiseStartGame()
            => OnStartGame?.Invoke();

        public static void ResetEvents()
        {
                OnGameOver = null;
                OnGameWin = null;
                OnCellChecked = null;
                OnStartGame = null;
        }

}
