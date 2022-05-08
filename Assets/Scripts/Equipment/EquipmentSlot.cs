﻿using Equipment.Interfaces;
using UnityEngine;

namespace Equipment
{
    public abstract class EquipmentSlot<T> : MonoBehaviour where T : IEquippable
    {
        private protected Transform Transform;
        private protected T Equipment;

        public abstract void Init();
        public abstract Transform GetEquipSlotTransform();
        public abstract void SetEquipmentInSlot(T equipment);
        public abstract bool IsSlotAvailable();
    }
}