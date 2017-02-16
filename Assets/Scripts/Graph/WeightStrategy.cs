using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface WeightStrategy<T> {
    float CalcuateWeight(Node<T> a, Node<T> b);
}

