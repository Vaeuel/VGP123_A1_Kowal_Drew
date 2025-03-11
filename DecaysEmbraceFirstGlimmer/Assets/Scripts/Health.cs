using UnityEngine;

public class Health : MonoBehaviour
{
    private int maxLives = 10;
    private int _lives = 5;

    public int lives //Provides a public variable that can be called like **Health.lives++;** which will run the below function with a value of 1
    {
        get => _lives;
        set
        {
            _lives = value; //Value is defined during the function call. 
            if (_lives > maxLives) _lives = maxLives;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
