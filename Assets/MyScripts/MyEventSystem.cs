public static class MyEventSystem 
{
    public static System.Action OnGameOver;
    public static System.Action OnGameWin;

    public static void RaiseTryGameOver()
            => OnGameOver?.Invoke();

    public static void RaiseTryGameWin()
            => OnGameWin?.Invoke();

}
