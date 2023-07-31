#ifndef GRP_MACRO_MATH_H
    #define GRP_MACRO_MATH_H

    float Square(float x) {
        return x * x;
    }

    float DistanceSquared(float3 pA, float3 pB) {
        return dot(pA - pB, pA - pB);
    }

#endif