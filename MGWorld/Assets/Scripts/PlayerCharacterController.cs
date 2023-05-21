using UnityEngine;
using UnityEngine.Events;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Collections.Generic;

namespace MyGame
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInputHandler))]
    public class PlayerCharacterController : MonoBehaviour
    {

        [Header("Movement")] [Tooltip("Max movement speed when grounded (when not sprinting)")]
        public float MaxSpeedOnGround = 10f;
        public bool CanMove = true;
        public int MaxChatCount = 6;

        CharacterController m_Controller;
        PlayerInputHandler m_InputHandler;


        int m_ChatCount = 0;
        bool m_Chattable = false;
        bool m_Chatting = false;
        string m_Name;
        Dictionary<string, string> m_ChatHistory = new Dictionary<string, string>();
        Dictionary<string, string> m_Character = new Dictionary<string, string>();
        
        public string remoteFilePath = "/root/Muse/bot/query.txt";
        public string remoteFileBackPath = "/root/Muse/bot/response.txt";
        public string serverCommand = "python3 test.py";

        void Start()
        {
            // fetch components on the same gameObject
            m_Controller = GetComponent<CharacterController>();
            m_InputHandler = GetComponent<PlayerInputHandler>();
            m_Controller.enableOverlapRecovery = true;

            EventManager.AddListener<ChatBackEvent>(OnChatBack);

            // Connect("呃", "请你写一篇一百字的作文！");
        }

        void Update()
        {
            if (CanMove)
            {
                m_Controller.Move(m_InputHandler.GetMoveInput() * MaxSpeedOnGround * Time.deltaTime);
            }

            if (m_Chatting)
            {
                if (m_InputHandler.GetChat())
                {
                    // ChatOver();
                }
            }
            else if (m_Chattable)
            {
                if (m_InputHandler.GetChat())
                {
                    Chat();
                }
            }
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "NPC")
            {
                Approach(collider.gameObject.name);
                m_Name = collider.gameObject.name;
            }
        }

        void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.tag == "NPC")
            {
                ApproachOver(collider.gameObject.name);
                if (m_Chatting)
                {
                    ChatOver();
                }
            }
        }

        void OnChatBack(ChatBackEvent evt)
        {
            if (evt.Type == ChatType.Read)
            {
                UnityEngine.Debug.Log("Read");
            }
            else if (evt.Type == ChatType.Options)
            {
                UnityEngine.Debug.Log("Click option" + evt.Option + " " + evt.Chat);
                UpdateHistory(m_Name, "赫鲁：" + evt.Chat);
            }
            else if (evt.Type == ChatType.Input)
            {
                UnityEngine.Debug.Log("Input " + evt.Chat);
                UpdateHistory(m_Name, "赫鲁：" + evt.Chat);
            }
            UnityEngine.Debug.Log(QueryHistroy(m_Name));
            ChatOver();
            m_ChatCount++;
            if (m_ChatCount == MaxChatCount)
            {
                m_ChatCount = 0;
            }
            else
            {
                Chat();
            }
        }

        private void Chat()
        {
            int r = Random.Range(0, 100);
            ChatEvent evt;
            string chat = QueryHistroy(m_Name);
            string job = QueryCharacter(m_Name) + m_Name;
            string result;
            string option1;
            string option2;
            if (m_ChatCount == MaxChatCount - 1)
            {
                result = Connect(m_Name, chat + "根据以上对话，如果你是" + m_Name + "，请你对赫鲁说一句话，不多于100字。请注意，赫鲁是失忆的战斗机器人，背景是有关科技创新的未来世界，但是是末世，人类与机器人展开了惨痛的战斗。人类是弱势的一方，机器人正在追杀人类。不要忘记你是" + job, 6000);
                // result = "喵喵喵";
                evt = new ChatEvent(m_Name, job, result);
                UpdateHistory(m_Name, m_Name + "：" + result);
            }
            else
            {
                if (r < 15)
                {
                    result = Connect(m_Name, chat + "根据以上对话，如果你是" + m_Name + "，请你对赫鲁说一句话，不多于100字。请注意，赫鲁是失忆的战斗机器人，背景是有关科技创新的未来世界，但是是末世，人类与机器人展开了惨痛的战斗。人类是弱势的一方，机器人正在追杀人类。不要忘记你是" + job, 6000);
                    // result = "护理";
                    evt = new ChatEvent(m_Name, job, result);
                    UpdateHistory(m_Name, m_Name + "：" + result);
                }
                else if (r < 70)
                {
                    // result = "狗";
                    result = Connect(m_Name, chat + "根据以上对话，如果你是" + m_Name + "，请你对赫鲁说一句话，不多于100字。请注意，赫鲁是失忆的战斗机器人，背景是有关科技创新的未来世界，但是是末世，人类与机器人展开了惨痛的战斗。人类是弱势的一方，机器人正在追杀人类。不要忘记你是" + job, 6000);
                    option1 = Connect(m_Name, chat + result + "\n" + "根据以上对话，如果你是赫鲁，请你对" + job + "说一句话，尽量正常一点，绝对不要多于10个字，要少于10个字。不要忘记你是失忆的战斗机器人。", 4000);
                    option2 = Connect(m_Name, chat + result + "\n" + "根据以上对话，如果你是赫鲁，请你对" + job + "说一句话，尽量搞怪一点，绝对不要多于10个字，要少于10个字。不要忘记你是失忆的战斗机器人。", 4000);
                    evt = new ChatEvent(m_Name, job, result, option1, option2);
                    UpdateHistory(m_Name, m_Name + "：" + result);
                }
                else
                {
                    // result = "狼";
                    result = Connect(m_Name, chat + "根据以上对话，如果你是" + m_Name + "，请你对赫鲁说一句话，不多于100字。请注意，赫鲁是失忆的战斗机器人，背景是有关科技创新的未来世界，但是是末世，人类与机器人展开了惨痛的战斗。人类是弱势的一方，机器人正在追杀人类。不要忘记你是" + job, 6000);
                    evt = new ChatEvent(m_Name, job, result, "");
                    UpdateHistory(m_Name, m_Name + "：" + result);
                }
            }
            EventManager.Broadcast(evt);
            CanMove = false;
            m_Chatting = true;
        }

        private void ChatOver()
        {
            ChatOverEvent evt = Events.ChatOverEvent;
            evt.Name = m_Name;
            EventManager.Broadcast(evt);
            CanMove = true;
            m_Chatting = false;
        }

        private void Approach(string name)
        {
            ApproachEvent evt = Events.ApproachEvent;
            evt.Name = name;
            EventManager.Broadcast(evt);
            m_Chattable = true;
        }

        private void ApproachOver(string name)
        {
            ApproachOverEvent evt = Events.ApproachOverEvent;
            evt.Name = name;
            EventManager.Broadcast(evt);
            m_Chattable = false;
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<ChatBackEvent>(OnChatBack);
        }

        private string Connect(string name, string chat, int time)
        {
            string localFilePath = Application.persistentDataPath + "/" + name;
            System.IO.File.WriteAllText(localFilePath, chat);

            // 拷贝文件到远程服务器
            ProcessStartInfo scpInfo = new ProcessStartInfo("scp", localFilePath + " root@connect.neimeng.seetacloud.com:" + remoteFilePath);
            scpInfo.UseShellExecute = true;
            Process scpProcess = Process.Start(scpInfo);
            scpProcess.WaitForExit();

            Thread.Sleep(time);

            // 从远程服务器下载生成的结果文件
            ProcessStartInfo scpDownloadInfo = new ProcessStartInfo("scp", "root@connect.neimeng.seetacloud.com:" + remoteFileBackPath + " " + localFilePath + "_result");
            scpDownloadInfo.UseShellExecute = true;
            Process scpDownloadProcess = Process.Start(scpDownloadInfo);
            scpDownloadProcess.WaitForExit();

            // 读取结果文件并进行处理
            string result = System.IO.File.ReadAllText(localFilePath + "_result");

            UnityEngine.Debug.Log(result);
            return result;
        }

        void UpdateHistory(string name, string chat)
        {
            if (m_ChatHistory.ContainsKey(name))
            {
                m_ChatHistory[name] += chat + '\n';
            }
            else
            {
                m_ChatHistory.Add(name, chat + '\n');
            }
        }

        string QueryHistroy(string name)
        {
            if (m_ChatHistory.ContainsKey(name))
            {
                return m_ChatHistory[name];
            }
            else
            {
                return "";
            }
        }

        string QueryCharacter(string name)
        {
            if (m_Character.ContainsKey(name))
            {
                return m_Character[name];
            }
            else
            {
                int r = Random.Range(0, 100);
                string character;
                if (name == "雪莉")
                {
                    character = "在战火中失去父母的孤儿 ";
                }
                else if (r < 10)
                {
                    character = "神秘的";
                }
                else if (r < 15)
                {
                    character = "可爱的";
                }
                else if (r < 20)
                {
                    character = "温柔的";
                }
                else if (r < 25)
                {
                    character = "对世界感到绝望的";
                }
                else if (r < 30)
                {
                    character = "富豪 ";
                }
                else if (r < 35)
                {
                    character = "可爱的";
                }
                else if (r < 40)
                {
                    character = "少年 ";
                }
                else if (r < 45)
                {
                    character = "曾经和赫鲁是战友的";
                }
                else if (r < 50)
                {
                    character = "温柔的";
                }
                else if (r < 60)
                {
                    character = "人类平民 ";
                }
                else if (r < 70)
                {
                    character = "叛徒 ";
                }
                else if (r < 80)
                {
                    character = "机器人军队下属 ";
                }
                else if (r < 90)
                {
                    character = "人类军队指挥官 ";
                }
                else
                {
                    character = "赏金猎人";
                }
                m_Character.Add(name, character);
                return character;
            }
        }
    }
}