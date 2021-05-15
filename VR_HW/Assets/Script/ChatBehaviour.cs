using Mirror;
using System;
using TMPro;
using UnityEngine;

namespace DapperDino.Mirror.Tutorials.Chat
{
    public class ChatBehaviour : NetworkBehaviour
    {
        public GameObject chatUI = null;
        public TMP_Text chatText = null;
        public TMP_InputField inputField = null;
        private String playerName = "";

        private static event Action<string> OnMessage;

        public override void OnStartAuthority()
        {
            chatUI.SetActive(true);


            NetworkGamePlayerLobby[] gamePlayers = FindObjectsOfType<NetworkGamePlayerLobby>();
            for (int i = 0; i < gamePlayers.Length; i++)
            {
                if (gamePlayers[i].hasAuthority)
                {
                    playerName = gamePlayers[i].displayName;
                    Debug.Log("chat : my name is " + playerName);
                }
            }

            OnMessage += HandleNewMessage;
        }

        [ClientCallback]
        private void OnDestroy()
        {
            if (!hasAuthority) { return; }

            OnMessage -= HandleNewMessage;
        }

        private void HandleNewMessage(string message)
        {
            chatText.text += message;
        }

        [Client]
        public void Send(string message)
        {
            if (!Input.GetKeyDown(KeyCode.Return)) { return; }

            if (string.IsNullOrWhiteSpace(message)) { return; }

            CmdSendMessage(message,playerName);

            inputField.text = string.Empty;
        }

        [Command]
        private void CmdSendMessage(string message,string player)
        {
            
            RpcHandleMessage($"[{player}]: {message}");
        }

        [ClientRpc]
        private void RpcHandleMessage(string message)
        {
            OnMessage?.Invoke($"\n{message}");
        }
    }
}
