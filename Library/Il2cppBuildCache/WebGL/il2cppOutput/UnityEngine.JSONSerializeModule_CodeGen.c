#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include "codegen/il2cpp-codegen-metadata.h"





// 0x00000001 System.Object UnityEngine.JsonUtility::FromJsonInternal(System.String,System.Object,System.Type)
extern void JsonUtility_FromJsonInternal_m12FFC156B7B2B3BD8749963BF4E9450F23CB502C (void);
// 0x00000002 T UnityEngine.JsonUtility::FromJson(System.String)
// 0x00000003 System.Object UnityEngine.JsonUtility::FromJson(System.String,System.Type)
extern void JsonUtility_FromJson_m9702DE524E4A879007AAC6F7D9798560628E7D0E (void);
static Il2CppMethodPointer s_methodPointers[3] = 
{
	JsonUtility_FromJsonInternal_m12FFC156B7B2B3BD8749963BF4E9450F23CB502C,
	NULL,
	JsonUtility_FromJson_m9702DE524E4A879007AAC6F7D9798560628E7D0E,
};
static const int32_t s_InvokerIndices[3] = 
{
	2029,
	-1,
	2217,
};
static const Il2CppTokenRangePair s_rgctxIndices[1] = 
{
	{ 0x06000002, { 0, 2 } },
};
static const Il2CppRGCTXDefinition s_rgctxValues[2] = 
{
	{ (Il2CppRGCTXDataType)1, 111 },
	{ (Il2CppRGCTXDataType)2, 111 },
};
extern const CustomAttributesCacheGenerator g_UnityEngine_JSONSerializeModule_AttributeGenerators[];
IL2CPP_EXTERN_C const Il2CppCodeGenModule g_UnityEngine_JSONSerializeModule_CodeGenModule;
const Il2CppCodeGenModule g_UnityEngine_JSONSerializeModule_CodeGenModule = 
{
	"UnityEngine.JSONSerializeModule.dll",
	3,
	s_methodPointers,
	0,
	NULL,
	s_InvokerIndices,
	0,
	NULL,
	1,
	s_rgctxIndices,
	2,
	s_rgctxValues,
	NULL,
	g_UnityEngine_JSONSerializeModule_AttributeGenerators,
	NULL, // module initializer,
	NULL,
	NULL,
	NULL,
};
