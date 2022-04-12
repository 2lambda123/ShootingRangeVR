﻿using System;
using UnityEngine;
using Valve.VR;
using Weapon;

namespace Nade
{
    public class FuseTrigger : MonoBehaviour
    {
        public event Action OnRelease;

        [SerializeField] private SteamVR_Action_Single _grip;

        private Attachment _attachment;
        private bool _isInit;

        public void Init()
        {
            _attachment = GetComponent<Attachment>();
            _isInit = true;
        }

        private void Update()
        {
            if (!_isInit) return;
            if (!_attachment.TryGetHand(out var hand)) return;

            var type = hand.handType;

            if (!(_grip[type].axis < 0.25f)) return;

            Debug.Log("Grip Released");
            OnRelease?.Invoke();
        }
    }
}