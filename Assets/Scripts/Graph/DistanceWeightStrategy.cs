using UnityEngine;

public class DistanceWeightStrategy<T> : WeightStrategy<T> where T : Cartesian  {
    public float CalcuateWeight(Node<T> a, Node<T> b) {

        //Just cartesion distance
        return Mathf.Sqrt(
            Mathf.Pow(
                a.GetSubject().GetCoordinates().x - b.GetSubject().GetCoordinates().x, 2f) +
            Mathf.Pow(
                a.GetSubject().GetCoordinates().y - b.GetSubject().GetCoordinates().y, 2f));
    }
}

