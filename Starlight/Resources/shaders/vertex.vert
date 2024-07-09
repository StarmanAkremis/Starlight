#version 330 core
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTexCoord;

out vec2 TexCoord;

uniform mat4 transform;
uniform float maxoffset;
uniform float randhelper;

float random(float seed) {
    return fract(sin(seed*vec2(12.9898,78.233)) * 43758.5453123 * sin(seed*vec2(12.9898,78.233)) * 43758.5453123);
}

void main()
{
    float xoffset = random(aPos.x * aPos.y * randhelper);
    float yoffset = random(aPos.x * aPos.y * randhelper);
    float zoffset = random(aPos.x * aPos.y * randhelper);

    vec3 newpos = aPos + (xoffset, yoffset, zoffset);

    gl_Position = vec4(newpos, 1.0) * transform;
    TexCoord = aTexCoord;
}
