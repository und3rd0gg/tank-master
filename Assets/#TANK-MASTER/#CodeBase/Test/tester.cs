using System;
using Dreamteck.Splines;
using UnityEngine;

namespace TankMaster.Test
{
    public class tester : MonoBehaviour
    {
        [SerializeField] private SplineComputer _splineMain;
        [SerializeField] private SplineComputer _spline2;

        private void Start() {
            var points = _spline2.GetPoints();
            var lastPointIndex = _splineMain.GetPoints().Length;

            int index = lastPointIndex - 1;

            foreach (SplinePoint point in points) {
                _splineMain.SetPoint(++index, point);
            }
        }
    }
}