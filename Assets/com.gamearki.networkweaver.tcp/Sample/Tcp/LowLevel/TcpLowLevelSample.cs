using System;
using System.Collections;
using UnityEngine;

namespace GameArki.Network.Sample {

    public class TcpLowLevelSample : MonoBehaviour {

        const int PORT = 9205;

        TcpLowLevelClient client;
        TcpLowLevelServer server;

        bool isConnected;

        void Awake() {

            server = new TcpLowLevelServer();
            server.Init(1024);
            server.StartListen(PORT);

            client = new TcpLowLevelClient();
            client.Init(1024);
            client.Connect("127.0.0.1", PORT);

        }

        void Start() {
            StartCoroutine(FakeInputIE());
        }

        void Update() {

            try {

                server.Tick();

                if (client.IsConnected()) {
                    client.Tick();
                }

            } catch {

                throw;

            }

        }

        IEnumerator FakeInputIE() {
            WaitForSeconds seconds = new WaitForSeconds(1f);
            while (enabled) {
                if (client.IsConnected()) {
                    client.Send(new byte[5] { 1, 2, 3, 4, 8 });
                }
                yield return seconds;
            }
        }

    }

}