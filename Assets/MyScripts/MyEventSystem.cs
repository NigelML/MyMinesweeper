public static class MyEventSystem
{
        public static System.Action OnGameOver;
        public static System.Action OnGameWin;
        public static System.Action OnCellChecked;

        public static void RaiseTryGameOver()
                => OnGameOver?.Invoke();

        public static void RaiseTryGameWin()
                => OnGameWin?.Invoke();
        public static void RaiseCellChecked()
            => OnCellChecked?.Invoke();

}
