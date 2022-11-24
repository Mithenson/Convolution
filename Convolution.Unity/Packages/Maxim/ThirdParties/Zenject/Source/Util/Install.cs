using Cysharp.Threading.Tasks;

namespace Zenject
{
	public delegate UniTask Install(DiContainer container);
	
	public delegate UniTask Install<in TParam1>(DiContainer container, TParam1 param1);
	
	public delegate UniTask Install<in TParam1, in TParam2>(DiContainer container, TParam1 param1, TParam2 param2);
	
	public delegate UniTask Install<in TParam1, in TParam2, in TParam3>(DiContainer container, TParam1 param1, TParam2 param2, TParam3 param3);

    public delegate UniTask Install<in TParam1, in TParam2, in TParam3, in TParam4>(DiContainer container, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4);

    public delegate UniTask Install<in TParam1, in TParam2, in TParam3, in TParam4, in TParam5>(DiContainer container, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5);

    public delegate UniTask Install<in TParam1, in TParam2, in TParam3, in TParam4, in TParam5, in TParam6>(DiContainer container, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6);

    public delegate UniTask Install<in TParam1, in TParam2, in TParam3, in TParam4, in TParam5, in TParam6, in TParam7, in TParam8, in TParam9, in TParam10>(
        DiContainer container, 
        TParam1 param1,
        TParam2 param2, 
        TParam3 param3,
        TParam4 param4, 
        TParam5 param5,
        TParam6 param6, 
        TParam7 param7,
        TParam8 param8,
        TParam9 param9,
        TParam10 param10);
}