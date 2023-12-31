﻿using UnityEngine;

namespace Enemy {
    [CreateAssetMenu(menuName = "Enemy/StartPrepare")]
    public class StartPrepareAction : Action {

        public override void Act(StateController controller) {
            EnemyController m = (EnemyController)controller;
            m.PrepareAttack();
        }
    }
}