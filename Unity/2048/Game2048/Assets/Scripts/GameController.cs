using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Model2048;
using System;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public Button start;
    public Text text;

    Model model = new Model(4);


    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            float newX = touchDeltaPosition.x;
            float newY = touchDeltaPosition.y;


            if (Math.Abs(newX) > Math.Abs(newY))
            {
                if (newX < 1)
                    model.Left();
                else
                    model.Right();
            }
            else
            {
                if (newY < 1)
                    model.Down();
                else
                    model.Up();
            }

            Show();
            if (model.IsGameOver())
                text.text = "GameOver!";

        }
    }

        public void StartGame()
        {
            model.Start();
            Show();
        }

        void Show()
        {
            for (int x = 0; x < model.size; x++)
            {
                for (int y = 0; y < model.size; y++)
                {
                    ShowButtonText("" + x + y, model.GetMap(x, y));
                }
            }
        }

        private void ShowButtonText(string name, int num)
        {
            var button = GameObject.Find(name);
            var text = button.GetComponentInChildren<Text>();
            text.text = num == 0 ? " " : num.ToString();
        }

    }

