﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
namespace Game
{
    public partial class PlayerController
    {
        public PlayerInfo PlayerInfo;
        [Header("属性的原生数值")]
        public int MaxHP;
        public int MaxElec;
        public int MaxStamina;
        public int STROrigin;
        public int DEXOrigin;
        public int TECOrigin;
        public int LUCOrigin;
        [Header("属性的实时数值")]
		public int HP;
		public int Elec;
		public int Stamina;
        public int Soul;
        public int STR;
        public int DEX;
        public int TEC;
        public int LUC;
        public int Level;
        public float STRFix { get { return CalcFixValue(STR); } }
        public float DEXFix { get { return CalcFixValue(DEX); } }
        public float TECFix { get { return CalcFixValue(TEC); } }
        public float LUCFix { get { return CalcFixValue(LUC); } }

        private float staminaRecoverConuntdown = 0f;
        public bool LoadDataFromFile = false;

        public int DamageToTake = 0;
        public string DamageTag = "";

        public void LockStamina() {
            staminaRecoverConuntdown = Constants.StaminaLockCountdown;
        }
        private void RecoverStamina(float deltatime) {
            if (Stamina >= MaxStamina) { 
                Stamina = MaxStamina;
                return;
            }
            if (staminaRecoverConuntdown <= 0) { 
                Stamina += 1;
            }
            else {
                staminaRecoverConuntdown -= deltatime;
            }
        }
        public void TakeDamage(int amount) {
            if (invinsibleOnHitTimer <= 0) {
                DamageToTake = amount;
                DamageTag = "";
                UpdateEquipsOnHurt();
                HP -= DamageToTake;
                if (HP <= 0) {
                    HP = 0;
                    Die();
                }
                invinsibleOnHitTimer = Constants.InvinsibleOnHitTime;
            }
        }
        public void TakeDamage(int amount, string tag) {
            if (invinsibleOnHitTimer <= 0) {
                DamageToTake = amount;
                DamageTag = tag;
                UpdateEquipsOnHurt();
                HP -= DamageToTake;
                if (HP <= 0) {
                    HP = 0;
                    Die();
                }
                invinsibleOnHitTimer = Constants.InvinsibleOnHitTime;
            }
        }
        public void RestoreHP(int amount) {
            HP += amount;
            if (HP > MaxHP) HP = MaxHP;
        }
        public void RestoreElec(int amount) {
            Elec += amount;
            if (Elec > MaxElec) Elec = MaxElec;
        }

        private PlayerInfo LoadFromFile() {
            PlayerInfo info = null;
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            path = path + "\\PlayerInfo.xml";
            if (System.IO.File.Exists(path)) {
                // 创建 XML 序列化器
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(PlayerInfo));
                // 创建文件流，用于读取 XML 数据
                using (System.IO.TextReader reader = new System.IO.StreamReader(path)) {
                    // 使用序列化器将对象数据读取出来
                    info = serializer.Deserialize(reader) as PlayerInfo;
                }
            }
            // 删除原文件
            System.IO.File.Delete(path);
            return info;
        }
        private float CalcFixValue(int value) {
            float result = 0;
            if (value <= 9 && value >= 0) result = value / 100f;
            if (value <= 29 && value >= 10) result = (10 + (value - 10) * 2) / 100f;
            if (value <= 39 && value >= 30) result = (50 + (value - 30) * 3.2f) / 100f;
            if (value <= 98 && value >= 40) result = (80 + (value - 40) * 0.328f) / 100f;
            if (value >= 99) result = 1f;
            return result;
        }
        private float CalcHPValue(int value) {
            float result = 0;
            if (value <= 9 && value >= 0) result = value / 100f;
            if (value <= 29 && value >= 10) result = (10 + (value - 10) * 2) / 100f;
            if (value <= 39 && value >= 30) result = (50 + (value - 30) * 3.2f) / 100f;
            if (value <= 98 && value >= 40) result = (80 + (value - 40) * 0.328f) / 100f;
            if (value >= 99) result = 1f;
            return result;
        }
        public int NextLevelExp(int currentLevel) {
            int result = 0;
            if (currentLevel + 81 < 92) {
                result = (int)(0.1f * Math.Pow(currentLevel + 81, 2) + 1);
            } else {
                result = (int)((0.1f + 0.02f * (currentLevel + 81 - 92)) * Math.Pow(currentLevel + 81, 2) + 1);
            }
            return result;
        }

        public void GainSoul(int amount) { 
            Soul += amount;
        }
        private void CalcFix() {
        }
        private void LoadPlayerInfo() {
            PlayerInfo = LoadFromFile();
            if (PlayerInfo == null || !LoadDataFromFile) {
                PlayerInfo = ScriptableObject.CreateInstance<PlayerInfo>();
                PlayerInfo.SaveData();
            }
            {
                MaxHP = PlayerInfo.MaxHP;
                MaxElec = PlayerInfo.MaxElec;
                MaxStamina = PlayerInfo.MaxStamina;
                STROrigin = PlayerInfo.STROrigin;
                DEXOrigin = PlayerInfo.DEXOrigin;
                TECOrigin = PlayerInfo.TECOrigin;
                LUCOrigin = PlayerInfo.LUCOrigin;
                HP = PlayerInfo.HP;
                Elec = PlayerInfo.Elec;
                Stamina = PlayerInfo.Stamina;
                Soul = PlayerInfo.Soul;
                STR = PlayerInfo.STR;
                DEX = PlayerInfo.DEX;
                TEC = PlayerInfo.TEC;
                LUC = PlayerInfo.LUC;
                Level = PlayerInfo.Level;
            }
            
        }
        private void SavePlayerInfo() { 
            PlayerInfo.MaxHP = MaxHP;
            PlayerInfo.MaxElec = MaxElec;
            PlayerInfo.MaxStamina = MaxStamina;
            PlayerInfo.STROrigin = STROrigin;
            PlayerInfo.DEXOrigin = DEXOrigin;
            PlayerInfo.TECOrigin = TECOrigin;
            PlayerInfo.LUCOrigin = LUCOrigin;
            PlayerInfo.HP = HP;
            PlayerInfo.Elec = Elec;
            PlayerInfo.Stamina = Stamina;
            PlayerInfo.Soul = Soul;
            PlayerInfo.STR = STR;
            PlayerInfo.DEX = DEX;
            PlayerInfo.TEC = TEC;
            PlayerInfo.LUC = LUC;
            PlayerInfo.Level = Level;
            PlayerInfo.SaveData();
        }
	}
}
