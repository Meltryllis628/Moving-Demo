using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Game;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UI {
    public class UIOverPlayer:MonoBehaviour {
        public PlayerController Player;
        private int HP;
        public Image HPBar;
        private int Elec;
        public Image ElecBar;
        private int Stamina;
        public Image StaminaBar;
        private int MaxHP;
        public Image MaxHPBar;
        private int MaxElec;
        public Image MaxElecBar;
        private int MaxStamina;
        public Image MaxStaminaBar;
        private int Soul;
        public TextMeshProUGUI SoulText;

        public Canvas DiePanel;
        private void Start() {
            Player = this.transform.parent.GetComponent<PlayerController>();

        }
        private string Len3(int value) {
            string result = "";
            if (value < 10) result = "00" + value.ToString();
            if (value >= 10 && value < 100) result = "0" + value.ToString();
            if (value >= 100) result = value.ToString();
            return result;
        }

        private string Int2String(int value) {
            string result = "";
            if (value < 1000) result = value.ToString();
            if (value >= 1000 && value < 1000000) result = (value / 1000).ToString() + "," + Len3(value % 1000);
            if (value >= 1000000 && value < 1000000000) result = (value / 1000000).ToString() + "," + Len3((value % 1000000) / 1000) + "K";
            return result;
        }
        private void UpdateBarValues() { 
            HP = Player.HP;
            Elec = Player.Elec;
            Stamina = Player.Stamina;
            MaxHP = Player.MaxHP;
            MaxElec = Player.MaxElec;
            MaxStamina = Player.MaxStamina;
            Soul = Player.Soul;
            HPBar.rectTransform.sizeDelta = new Vector2(HP * 0.8f + 20f, 25f);
            ElecBar.rectTransform.sizeDelta = new Vector2(Elec * 2f+20f, 25f);
            StaminaBar.rectTransform.sizeDelta = new Vector2(Stamina * 3f + 20f, 25f);
            MaxHPBar.rectTransform.sizeDelta = new Vector2(MaxHP * 0.8f + 35f, 40f);
            MaxElecBar.rectTransform.sizeDelta = new Vector2(MaxElec * 2f+35f, 40f);
            MaxStaminaBar.rectTransform.sizeDelta = new Vector2(MaxStamina * 3f+35f, 40f);
            SoulText.text = Int2String(Soul);
        }
        private void Update() {
            if (Player == null) return;
            if (Player.isAlive) {
                DiePanel.gameObject.SetActive(false);
            } else { 
                DiePanel.gameObject.SetActive(true);
            }
            UpdateBarValues();
        }
    }
}
