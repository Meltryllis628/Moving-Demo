﻿using Assets.Scripts.Attack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game {
    public partial class PlayerController {
        public bool CanAttack {
            get {
                return GameInput.AttackButton.Pressed() && onGround && !Ducking;
            }
        }

        public bool CanShoot {
            get {
                return GameInput.ShootButton.Pressed() && !Ducking;
            }
        }
        [HideInInspector]
        public int AttackFrames1 = 0;
        [HideInInspector]
        public float AttackFrames1MaxSpeed = 0f;
        [HideInInspector]
        public int AttackFrames2 = 0;
        [HideInInspector]
        public float AttackFrames2MaxSpeed = 0f;
        [HideInInspector]
        public int AttackFrames3 = 0;
        [HideInInspector]
        public float AttackFrames3MaxSpeed = 0f;
        [HideInInspector]
        public RangedAttack CurrentRangedAttack;
        [HideInInspector]
        public MeleeAttack CurrentMeleeAttack;
        [HideInInspector]
        public Weapon CurrentRangedWeapon;
        [HideInInspector]
        public Weapon CurrentMeleeWeapon;

        private int attackCount = 0;
        GameObject attack;

        public void SetUpWeapons(Weapon ranged, Weapon melee) {
            CurrentRangedWeapon = ranged;
            CurrentRangedWeapon.Player = this;
            Debug.Log(CurrentRangedWeapon + " Damage = "+CurrentRangedWeapon.BasicDamage);
            CurrentMeleeWeapon = melee;
            CurrentMeleeWeapon.Player = this;
            Debug.Log(CurrentMeleeWeapon + " Damage = " + CurrentMeleeWeapon.BasicDamage);
        }

        

        public EActionState Shoot(Weapon RangedWeapon) {
            CurrentRangedAttack = (RangedAttack)RangedWeapon.GetNextAttack(AttackKey.Shoot);
            AttackFrames1 = CurrentRangedAttack.ShootingAnimationFrames;
            AttackFrames1MaxSpeed = CurrentRangedAttack.ShootingAnimationMaxSpeed;
            AttackFrames2 = 0;
            AttackFrames3 = 0;
            CurrentRangedAttack.PerformAttack();
            return EActionState.Attack;
        }
        

        public EActionState Attack(Weapon MeleeWeapon) {
            CurrentMeleeAttack = (MeleeAttack)MeleeWeapon.GetNextAttack(AttackKey.Light);
            AttackFrames1 = CurrentMeleeAttack.BeforeAttackFrames;
            AttackFrames1MaxSpeed = CurrentMeleeAttack.BeforeAttackMaxSpeed;
            AttackFrames2 = CurrentMeleeAttack.AttackFrames;
            AttackFrames2MaxSpeed = CurrentMeleeAttack.AttackMaxSpeed;
            AttackFrames3 = CurrentMeleeAttack.AfterAttackFrames;
            AttackFrames3MaxSpeed = CurrentMeleeAttack.AfterAttackMaxSpeed;
            CurrentMeleeAttack.PerformAttack();
            return EActionState.Attack;
        }

        public void AttackEnd() {
            PlayAnimation("AttackEnd");
            CurrentMeleeWeapon.GetNextAttack(AttackKey.Break);
        }

    }
}