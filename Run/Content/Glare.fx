sampler TextureSampler : register(s0);

float4 PixelShader(float2 texCoord: TEXCOORD0) : COLOR
{
    // Look up the texture color.
    float4 tex = tex2D(TextureSampler, texCoord);
    float dist = abs(abs(texCoord.x - 0.5) - 0.5);

    
    float fact = dist / 0.5;
    float rdif = (1.0f - tex.rgb.r) * fact * 0.75;
    float bdif = (1.0f - tex.rgb.b) * fact * 0.75;
    float gdif = (1.0f - tex.rgb.g) * fact * 0.75;
    
    // The input color alpha controls saturation level.
    float4 color = float4(tex.rgb.r,tex.rgb.g,tex.rgb.b,1.0f);
    if(tex.rgb.r == 0.0)
	{
		if(tex.rgb.g <= 0.31 && tex.rgb.g >= 0.29)
		{
			color = float4(tex.rgb.r + rdif,tex.rgb.g + gdif,tex.rgb.b + bdif,1.0f);
		}
	}
	
    
    return color;
}


technique Desaturate
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShader();
    }
}