using System.Collections;
using UnityEngine;
using GameArki.BufferIO;

namespace GameArki.Network.Sample {

    public class TcpSample : MonoBehaviour {

        const int PORT = 9205;

        TcpClient client;
        TcpServer server;

        void Awake() {

            // ==== 创建服务端 ====
            // ==== 创建服务端 ====
            // ==== 创建服务端 ====
            server = new TcpServer();
            server.StartListen(PORT);

            // 服务端: 监听连接事件
            server.OnConnectedHandle += (int connID) => {

            };

            // 服务端: 监听断开连接事件
            server.OnDisconnectedHandle += (int connID) => {

            };

            // 服务端: 监听消息
            server.On(1, 1, () => new MyModel(), (connId, msg) => {
                Debug.Log("SERVER RECV: " + msg.intValue);

                // 发送消息给客户端
                server.Send(1, 2, connId, new HerModel());
            });

            // ==== 创建客户端 ====
            // ==== 创建客户端 ====
            // ==== 创建客户端 ====
            client = new TcpClient();
            client.Connect("127.0.0.1", PORT);

            // 客户端: 监听连接事件
            client.OnConnectedHandle += () => {

            };

            // 客户端: 监听断开连接事件
            client.OnDisconnectedHandle += () => {

            };

            // 客户端: 监听消息
            client.On(1, 2, () => new HerModel(), (msg) => {

            });

        }

        void Start() {
            StartCoroutine(FakeInputIE());
        }

        void Update() {

            // 服务端 Tick
            server.Tick();

            // 客户端 Tick
            if (client.IsConnected()) {
                client.Tick();
            }

        }

        // 模拟用户输入
        IEnumerator FakeInputIE() {
            WaitForSeconds seconds = new WaitForSeconds(1f);
            while (enabled) {
                if (client.IsConnected()) {
                    client.Send(1, 1, new MyModel { intValue = Random.Range(0, 100) });
                }
                yield return seconds;
            }
        }

    }

}