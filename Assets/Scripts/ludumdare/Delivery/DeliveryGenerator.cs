﻿using System.Collections.Generic;
using LudumDare.TimeControl;
using UnityEngine;

namespace LudumDare.Delivery
{
    
    public class DeliveryGenerator: MonoBehaviour
    {
        [SerializeField]
        private TimeControlManagerSocket controlManagerSocket;

        [SerializeField]
        private DeliveryResolver resolver;
        
        public void DoGenerate(int cycle)
        {
            if (cycle == 0) return;
            resolver.ExecuteDelivery(new List<DeliveryCommand> {new DeliveryCommand() {}});
        }
    }
}