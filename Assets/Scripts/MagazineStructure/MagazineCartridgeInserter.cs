﻿using Ammunition.CartridgeStructure;
using StructureComponents;
using UnityEngine;

namespace MagazineStructure
{
    [RequireComponent(typeof(Collider))]
    internal class MagazineCartridgeInserter : MonoBehaviour
    {
        private MagazineCartridgeContainer _container;
        private Attachment _attachment;
        private Cartridge _cartridge;

        public void Init(MagazineCartridgeContainer container, Attachment attachment)
        {
            _container = container;
            _attachment = attachment;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_cartridge != null) return;
            if (!other.TryGetComponent<Cartridge>(out var cartridge)) return;
            if (!cartridge.GetCartridgeAttachment().TryGetHand(out var hand)) return;

            _cartridge = cartridge;

            var attachment = _cartridge.GetCartridgeAttachment();
            attachment.OnDrop += OnCartridgeDrop;
        }

        private void OnCartridgeDrop()
        {
            if (!_attachment.TryGetHand(out var hand)) return;

            var magazineCaliber = _container.GetCaliber();
            var cartridgeCaliber = _cartridge.GetCartridgeType().Caliber;
            if (!magazineCaliber.Equals(cartridgeCaliber)) return;

            if (!_container.TryPush(_cartridge)) return;

            var attachment = _cartridge.GetCartridgeAttachment();
            attachment.OnDrop -= OnCartridgeDrop;
            _cartridge.Deactivate();
            _cartridge = null;
        }

        private void OnTriggerExit(Collider other)
        {
            if (_cartridge == null) return;

            if (!other.TryGetComponent<Cartridge>(out var cartridge)) return;
            if (_cartridge != cartridge) return;

            var attachment = _cartridge.GetCartridgeAttachment();
            attachment.OnDrop -= OnCartridgeDrop;
            _cartridge = null;
        }
    }
}