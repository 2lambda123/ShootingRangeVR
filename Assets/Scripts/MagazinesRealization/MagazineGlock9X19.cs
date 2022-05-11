﻿using MagazineStructure;
using StructureComponents;
using UnityEngine;

namespace MagazinesRealization
{
    public class MagazineGlock9X19 : Magazine
    {
        public virtual void Init()
        {
            MagazineCartridgeContainer = GetComponent<MagazineCartridgeContainer>();
            MagazineCartridgeExtractor = GetComponent<MagazineCartridgeExtractor>();
            MagazinePopper = GetComponent<Popper>();
            MagazineAttachment = GetComponent<Attachment>();
            Rigidbody = GetComponent<Rigidbody>();

            MagazineCartridgeContainer.Init();
            MagazineAttachment.Init();
            MagazinePopper.Init(MagazineAttachment);
            MagazineCartridgeExtractor.Init(MagazineCartridgeContainer, MagazinePopper, MagazineAttachment);
            MagazineCartridgeInserter.Init(MagazineCartridgeContainer, MagazineAttachment);
            MagazineView.Init(MagazineCartridgeContainer);
        }

        private void Start()
        {
            Init();
        }
    }
}