﻿using Round;
using UnityEngine;

namespace Weapon
{
    public class Extractor : MonoBehaviour
    {
        [SerializeField] private Transform _extractTransform;

        private Container<Magazine, MagazineType> _container;
        private Attachment _attachment;
        private Popper _popper;

        public void Init(Container<Magazine, MagazineType> container, Popper popper, Attachment attachment)
        {
            _container = container;
            _attachment = attachment;
            _popper = popper;
            _popper.OnButtonPressed += OnButtonPressed;
        }

        private void OnButtonPressed()
        {
            if (!_container.TryPop(out var magazine)) return;
            _attachment.TryGetHand(out var hand);

            var magazineTransform = magazine.transform;
            magazineTransform.position = _extractTransform.position;
            magazineTransform.rotation = _extractTransform.rotation;

            var rb = magazine.GetComponent<Rigidbody>();
            rb.velocity = hand.GetTrackedObjectVelocity();
            rb.angularVelocity = hand.GetTrackedObjectAngularVelocity();

            magazine.Activate();
        }
    }
}