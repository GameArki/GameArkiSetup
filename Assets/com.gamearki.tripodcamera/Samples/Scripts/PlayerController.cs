using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameArki.TripodCamera.Sample {

    public class PlayerController : MonoBehaviour {

        public float speed = 5;
        public float rotSpeed = 5;
        public float jumpSpeed = 5;

        public float timer;
        public bool isJumpingDown;

        void Start() {

        }

        void Update() {
            if (timer != 0f) {
                timer += UnityEngine.Time.deltaTime;
            }

            var forward = Camera.main.transform.forward;
            forward.y = 0;
            forward.Normalize();
            var right = Camera.main.transform.right;
            right.y = 0;
            right.Normalize();
            Vector3 moveInput = new Vector3();
            if (Input.GetKey(KeyCode.W)) {
                moveInput += forward * speed;
            }
            if (Input.GetKey(KeyCode.S)) {
                moveInput -= forward * speed;
            }
            if (Input.GetKey(KeyCode.A)) {
                moveInput -= right * speed;
            }
            if (Input.GetKey(KeyCode.D)) {
                moveInput += right * speed;
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                isJumpingDown = timer != 0 && timer < .3f;
                timer = UnityEngine.Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.Space)) {
                moveInput.y = isJumpingDown ? -jumpSpeed : jumpSpeed;
            }

            transform.position += moveInput * UnityEngine.Time.deltaTime;
            moveInput.y = 0;
            if (moveInput != Vector3.zero) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveInput), rotSpeed * UnityEngine.Time.deltaTime);
        }

    }
}
