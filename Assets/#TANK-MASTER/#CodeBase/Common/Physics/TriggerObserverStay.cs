using System;
using UnityEngine;

namespace TankMaster.Common.Physics
{
  public class TriggerObserverStay : TriggerObserver
  {
    public event Action<Collider> TriggerStay;
        
    private void OnTriggerStay(Collider other) =>
      TriggerStay?.Invoke(other);
  }
}