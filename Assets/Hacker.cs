using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
    //Variables and constants that will help us play our game
    //menuHint will tell the user that they can type menu whenever they want

    const string menuHint = "You can type menu at any time";
    //Class attributes
    //These arrays will hold the password of the different game's levels
    string[] level1Passwords = { "book", "class", "teacher", "room", "hour" };

    string[] answers1 = {"shelf", "elf", "on", "the", "aa"};

    string[] level2Passwords = { "cashier", "department", "payment", "electronics"};
    string[] level3Passwords = { "dossier", "international", "security", "terrorism"};

    //Here I declare an enumerated type to represent the different game states
    //and I declare a variable to hold the current game state
    int level;
    int position; //Whatever it is in this position is going to be the answer
    string answer;
    bool play = false;
    enum GameState {MainMenu, Password, Win}; //Refactor
    GameState currentScreen = GameState.MainMenu;
    string password;                //Will hold the password to be cracked

    // Use this for initialization 
    void Start () {
        ShowMainMenu();
	}

    // Update is called once per frame
    void Update () {
		
	}


    void ShowMainMenu()
    {
        //Only works with WM2000
        //We clear the screen
        Terminal.ClearScreen();

        //We show the menu
        Terminal.WriteLine("What do you want to hack today?");
        Terminal.WriteLine("");
        Terminal.WriteLine("1. Town's college");
        Terminal.WriteLine("2. City's Super Center");
        Terminal.WriteLine("3. NSA Server");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter your selection");
        //We set our current screen state as the main menu
        currentScreen = GameState.MainMenu;
    }

    void OnUserInput(string input)
    {
        //If user inputs the "menu" keyword, then we call the ShowMainMenu() method once more
        if(input == "menu")
        {
            ShowMainMenu();
        }
        //However if the user types quit, close or exit, then we try to close the game. If the game is played on a Web browser, then 
        //we ask the user to close the tab
        else if(input == "quit" || input == "close" || input == "exit")
        {
            Terminal.WriteLine("Please, close the browser's tab");
            //To end a game
            Application.Quit();
        }
        //If the user inputs anything that is not menu, quit, close or exit, then we are going to handle that input depending on the game state
        //If the game state is still MainMenu, then we call the RunMainMenu()
        else if(currentScreen == GameState.MainMenu)
        {
            RunMainMenu(input);
        }
        //But if the current state is password, then we call the CheckPassword() Method
        else if (currentScreen == GameState.Password)
        {
            CheckPassword(input);
        }
    }

    void CheckPassword(string input)
    {
        if(input == answer)//AQUI AQUI AQUI AQUI
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        play = false;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("1");
                break;
            case 2:
                Terminal.WriteLine("2");
                break;
            case 3:
                Terminal.WriteLine("3");
                break;
            default:
                Debug.LogError("Invalid level reached.");
                break;
        }
    }

    void RunMainMenu(string input)
    {
        //We first check that the input is a valid input
        bool isValidInput = (input == "1") || (input == "2") || (input == "3");
        // If the user iputs a valid level, we convert that input to an int value and then we call the AskForPassword() methos
        if (isValidInput)
        {
            level = int.Parse(input); //Change value from string to int
            AskForPassword();
        }
        //However if the user did not enter a valid input, then we validate for our Easter Egg
        else if(input == "666")
        {
            Terminal.WriteLine("Welcome to hell. Insert a valid input you person.");
            Terminal.WriteLine("XOXO, Satan.");
        }
        else
        {
            Terminal.WriteLine("Enter a valid level");
        }
    }

    void AskForPassword()
    {
        //We set our current game state as Password
       currentScreen = GameState.Password;

        //We clear our terminal screen
        Terminal.ClearScreen();

        //We call the SetRandomPassword() method
        if (!play)
        {
            SetRandomPassword();
            play = true;
        }

        Terminal.WriteLine("Enter your password. Hint: " + password);
        Terminal.WriteLine(menuHint);
    }

    void SetRandomPassword()
    {
        switch (level) {
            case 1:
                password = level1Passwords[UnityEngine.Random.Range(0,level1Passwords.Length-1)];
                position = SearchPosition(password, level1Passwords);
                answer = answers1[position];
                break;
            case 2:
                password = level2Passwords[UnityEngine.Random.Range(0, level2Passwords.Length-1)];
                position = SearchPosition(password, level2Passwords);
                answer = answers1[position];
                break;
            case 3:
                password = level3Passwords[UnityEngine.Random.Range(0, level3Passwords.Length-1)];
                position = SearchPosition(password, level3Passwords);
                answer = answers1[position];
                break;
            default:
                Debug.LogError("Invalid level. How did you manage that?");
                break;
        }
    }

    private int SearchPosition(string password, string[] levelPasswords)
    {
        for(int i = 0; i < levelPasswords.Length; i++){ //Ciclo for
            if(levelPasswords[i] == password){//Si el valor dentro de esa posicion de vector es igual al valor enviado
                return i;//Regresara la posicion
            }           
        }
        Terminal.WriteLine("Mistake");
        return -1;//Sino regresara -1 (posicion que no existe)
    }

}
