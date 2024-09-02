using ReducedScrabbleConsoleApp.Services;

GameConsoleService gameConsoleService = GameConsoleService.Instance;
gameConsoleService.Run();

//Available commands:
//c-exit : to exist the game process
//c-stats : to publish statistics
//c-reset : to reset the session
//any word input