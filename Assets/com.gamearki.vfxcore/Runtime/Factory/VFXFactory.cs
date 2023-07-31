namespace GameArki.VFX {

    internal static class VFXFactory {

        internal static VFXPlayerEntity CreateVFXPlayerEntity(string clipName) {
            var entity = new VFXPlayerEntity();
            entity.Init();
            return entity;
        }

    }
    
}