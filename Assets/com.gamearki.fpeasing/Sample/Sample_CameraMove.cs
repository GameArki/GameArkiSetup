using System;
using UnityEditor;
using UnityEngine;

namespace GameArki.FPEasing.Sample {
    
    public class Sample_CameraMove : MonoBehaviour
    {
        
        public int speed;
        public int rotate;
        
        Transform tf;
        
        void Awake() {

            tf = GetComponent<Transform>();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
        
        void Update() {

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            
            Move(new Vector2(horizontalInput, verticalInput));
            Rotate(mouseX,mouseY);

        }

        void Move(Vector2 dir) {

            Vector3 dir3 = new Vector3(dir.x, 0,dir.y);
            Vector3 next = tf.position + Time.deltaTime * speed * tf.TransformDirection(dir3);
            
            tf.position = next;

        }

        void Rotate(float mouseX , float mouseY) {

            float x = tf.eulerAngles.x + -(mouseY * rotate * Time.deltaTime);
            float y = tf.eulerAngles.y + mouseX * rotate * Time.deltaTime;

            if (x < -180f) {
                x += 360f;
            }
            if (x > 180f) {
                x -= 360f;
            }

            x = Math.Clamp(x,-90f,90f);
            tf.localEulerAngles = new Vector3(x,y,0);
            
        }
    }
}