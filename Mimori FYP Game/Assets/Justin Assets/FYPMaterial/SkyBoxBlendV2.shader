Shader "Custom/SkyBoxBlendV2" {
	Properties {
	_Tint ("Tint Color", Color) = (.5, .5, .5, .5)
    _Blend ("Blend", Range(0.0,3.0)) = 0.5

    _FrontTex ("Front (+Z)", 2D) = "white" {}
    _BackTex ("Back (-Z)", 2D) = "white" {}
    _LeftTex ("Left (+X)", 2D) = "white" {}
    _RightTex ("Right (-X)", 2D) = "white" {}
    _UpTex ("Up (+Y)", 2D) = "white" {}
    _DownTex ("Down (-Y)", 2D) = "white" {}

    _FrontTex2("2 Front (+Z)", 2D) = "white" {}
    _BackTex2("2 Back (-Z)", 2D) = "white" {}
    _LeftTex2("2 Left (+X)", 2D) = "white" {}
    _RightTex2("2 Right (-X)", 2D) = "white" {}
    _UpTex2("2 Up (+Y)", 2D) = "white" {}
    _DownTex2("2 Down (-Y)", 2D) = "white" {}

      _FrontTex3("3 Front (+Z)", 2D) = "white" {}
    _BackTex3("3 Back (-Z)", 2D) = "white" {}
    _LeftTex3("3 Left (+X)", 2D) = "white" {}
    _RightTex3("3 Right (-X)", 2D) = "white" {}
    _UpTex3("3 Up (+Y)", 2D) = "white" {}
    _DownTex3("3 Down (-Y)", 2D) = "white" {}

   	  _FrontTex4("4 Front (+Z)", 2D) = "white" {}
    _BackTex4("4 Back (-Z)", 2D) = "white" {}
    _LeftTex4("4 Left (+X)", 2D) = "white" {}
    _RightTex4("4 Right (-X)", 2D) = "white" {}
    _UpTex4("4 Up (+Y)", 2D) = "white" {}
    _DownTex4("4 Down (-Y)", 2D) = "white" {}
	}

	SubShader {
		 Tags { "Queue" = "Background" }
    Cull Off
    Fog { Mode Off }
    Lighting Off
    Color [_Tint]
    Pass {
        SetTexture [_FrontTex] { 
        combine texture 
        }
        SetTexture [_FrontTex2] { 
        constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous 
        }
        SetTexture [_FrontTex3] { 
        constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous 
        }
        SetTexture [_FrontTex4] { 
        constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous 
        }
    }
    Pass {
        SetTexture [_BackTex] { combine texture }
        SetTexture [_BackTex2] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
        SetTexture [_BackTex3] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
        SetTexture [_BackTex4] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
    }
    Pass {
        SetTexture [_LeftTex] { combine texture }
        SetTexture [_LeftTex2] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
        SetTexture [_LeftTex3] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
        SetTexture [_LeftTex4] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
    }
    Pass {
        SetTexture [_RightTex] { combine texture }
        SetTexture [_RightTex2] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
        SetTexture [_RightTex3] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
        SetTexture [_RightTex4] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
    }
    Pass {
        SetTexture [_UpTex] { combine texture }
        SetTexture [_UpTex2] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
         SetTexture [_UpTex3] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
          SetTexture [_UpTex4] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
    }
    Pass {
        SetTexture [_DownTex] { combine texture }
        SetTexture [_DownTex2] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
        SetTexture [_DownTex3] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
        SetTexture [_DownTex4] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
    }
}
}
