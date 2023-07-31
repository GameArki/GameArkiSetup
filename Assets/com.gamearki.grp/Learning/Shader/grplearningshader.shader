Shader "GRPLearning" {

    Properties {

    }

    SubShader {

        /*
        > AlphaToMask <state>
        AlphaToMask On
        AlphaToMask Off
        */

        /*
        > Blend <state>
        Blend Off
        > Blend <render target> <state>
        Blend 1 Off
        > Blend <source factor> <destination factor>
        Blend SrcAlpha OneMinusSrcAlpha
        */

        Pass {

        }
    }

    Fallback "Diffuse"

}