using GameArki.FreeInput.Facades;
using GameArki.FreeInput.API.Setter;
using GameArki.FreeInput.API.Getter;

namespace GameArki.FreeInput {

    public class FreeInputCore {

        FreeInputFacades facades;

        FreeInputSetter setter;
        public IFreeInputSetter Setter => setter;

        FreeInputGetter getter;
        public IFreeInputGetter Getter => getter;

        public FreeInputCore() {
            facades = new FreeInputFacades();
            setter = new FreeInputSetter();
            getter = new FreeInputGetter();
            setter.Inject(facades);
            getter.Inject(facades);
        }

    }

}