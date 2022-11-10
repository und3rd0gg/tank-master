using System;
using UnityEngine;

namespace TankMaster._CodeBase.Test
{
    public class TestController : MonoBehaviour
    {
        private const float Speed = 1.3f;

        private void Update()
        {
            MovementLogic();
        }

        private void MovementLogic()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            // что бы скорость была стабильной в любом случае
            // и учитывая что мы вызываем из FixedUpdate мы умножаем на fixedDeltaTimе
            transform.Translate(movement * Speed * Time.fixedDeltaTime);
        }
    }
}