using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public BaseMenu[] allMenus;

    public MenuStates initState = MenuStates.MainMenu;

    private BaseMenu currentState;

    Dictionary<MenuStates, BaseMenu> menuDictionary = new Dictionary<MenuStates, BaseMenu>();
    Stack<MenuStates> menuStack = new Stack<MenuStates>();

    void Start()
    {
        if(allMenus.Length <= 0)
        {
            allMenus = gameObject.GetComponentsInChildren<BaseMenu>(true);
        }
        foreach(BaseMenu menu in allMenus)
        {
            if(menu == null) continue;
            menu.Init(this);

            if(menuDictionary.ContainsKey(menu.state)) continue;

            menuDictionary.Add(menu.state, menu);
        }

        SetActiveState(initState);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }

    }

    public void TogglePauseMenu()
    {
        if(currentState == menuDictionary[MenuStates.Pause])
        {
            JumpBack(); // Return to previous menu
        }
        else
        {
            SetActiveState(MenuStates.Pause);
        }
    }

    public void JumpBack()
    {
        if (menuStack.Count <= 0) return;//Should add debug.log to track

        menuStack.Pop();//Pop takes the current item off of the stack.
        SetActiveState(menuStack.Peek(), true);
    }

    public void SetActiveState(MenuStates newState, bool isJumpingBack = false)
    {
        if (!menuDictionary.ContainsKey(newState)) return;

        if (currentState == menuDictionary[newState]) return;//Prevents attempting to load the same menu again

        if(currentState != null)
        {
            currentState.ExitState();
            currentState.gameObject.SetActive(false);
        }

        currentState = menuDictionary[newState];//Takes concrete menu from the dictionary and sets it to Current state.
        currentState.gameObject.SetActive(true);
        currentState.EnterState();

        if(!isJumpingBack) menuStack.Push(newState); //Adds new state to stack only when not trying to pop off the menu.
    }
}
