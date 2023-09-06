using GameArki.TripodCamera.Entities;

namespace GameArki.TripodCamera.Template {

    public static class TCTM2RuntimeUtil {

        public static TCShakeStateModel[] ToTCShakeStateModelArray(in TCShakeStateTM[] tmArray) {
            TCShakeStateModel[] modelArray = new TCShakeStateModel[tmArray.Length];
            for (int i = 0; i < tmArray.Length; i++) {
                modelArray[i] = ToTCShakeStateModel(tmArray[i]);
            }
            return modelArray;
        }

        public static TCShakeStateModel ToTCShakeStateModel(in TCShakeStateTM tm) {
            TCShakeStateModel model;
            model.amplitudeOffset = tm.amplitudeOffset;
            model.easingType = tm.easingType;
            model.frequency = tm.frequency;
            model.duration = tm.duration;
            return model;
        }
        public static TCFOVStateModel[] ToTCFOVStateModelArray(TCFOVStateTM[] tmArray) {
            TCFOVStateModel[] modelArray = new TCFOVStateModel[tmArray.Length];
            for (int i = 0; i < tmArray.Length; i++) {
                modelArray[i] = ToTCFOVStateModel(tmArray[i]);
            }
            return modelArray;
        }

        public static TCFOVStateModel ToTCFOVStateModel(in TCFOVStateTM tm) {
            TCFOVStateModel model;
            model.offset = tm.offset;
            model.duration = tm.duration;
            model.easingType = tm.easingType;
            model.isInherit = tm.isInherit;
            return model;
        }

        public static TCPushStateModel[] ToTCPushStateModelArray(TCPushStateTM[] tmArray) {
            TCPushStateModel[] modelArray = new TCPushStateModel[tmArray.Length];
            for (int i = 0; i < tmArray.Length; i++) {
                modelArray[i] = ToTCPushStateModel(tmArray[i]);
            }
            return modelArray;
        }

        public static TCPushStateModel ToTCPushStateModel(in TCPushStateTM tm) {
            TCPushStateModel model;
            model.offset = tm.offset;
            model.duration = tm.duration;
            model.easingType = tm.easingType;
            model.isInherit = tm.isInherit;
            return model;
        }

        public static TCRotateStateModel[] ToTCRotateStateModelArray(TCRotateStateTM[] tmArray) {
            TCRotateStateModel[] modelArray = new TCRotateStateModel[tmArray.Length];
            for (int i = 0; i < tmArray.Length; i++) {
                modelArray[i] = ToTCRotateStateModel(tmArray[i]);
            }
            return modelArray;
        }

        public static TCRotateStateModel ToTCRotateStateModel(in TCRotateStateTM tm) {
            TCRotateStateModel model;
            model.offset = tm.offset;
            model.duration = tm.duration;
            model.easingType = tm.easingType;
            model.isInherit = tm.isInherit;
            return model;
        }

        public static TCRoundStateModel[] ToTCRoundStateModelArray(TCRoundStateTM[] tm) {
            TCRoundStateModel[] modelArray = new TCRoundStateModel[tm.Length];
            for (int i = 0; i < tm.Length; i++) {
                modelArray[i] = ToTCRoundStateModel(tm[i]);
            }
            return modelArray;
        }

        public static TCRoundStateModel ToTCRoundStateModel(in TCRoundStateTM tm) {
            TCRoundStateModel model;
            model.offset = tm.offset;
            model.duration = tm.duration;
            model.easingType = tm.easingType;
            model.isInherit = tm.isInherit;
            return model;
        }

        public static TCMovementStateModel[] ToTCMovementStateModelArray(TCMovementStateTM[] tmArray) {
            TCMovementStateModel[] modelArray = new TCMovementStateModel[tmArray.Length];
            for (int i = 0; i < tmArray.Length; i++) {
                modelArray[i] = ToTCMovementStateModel(tmArray[i]);
            }
            return modelArray;
        }

        public static TCMovementStateModel ToTCMovementStateModel(in TCMovementStateTM tm) {
            TCMovementStateModel model;
            model.offset = tm.offset;
            model.duration = tm.duration;
            model.easingType = tm.easingType;
            model.isInherit = tm.isInherit;
            return model;
        }

        public static TCDollyTrackStateModel ToTCDollyTrackStateModel(in TCDollyTrackStateTM tm) {
            TCDollyTrackStateModel model;
            model.trackType = tm.trackType;
            model.bezierSlineModelArray = ToTCBezierSplineModelArray(tm.bezierSlineTMArray);
            var totalDuration = 0f;
            for (int i = 0; i < model.bezierSlineModelArray.Length; i++) {
                totalDuration += model.bezierSlineModelArray[i].duration;
            }
            model.totalDuraration = totalDuration;
            return model;
        }

        public static TCBezierSplineModel ToTCBezierSplineTM(in TCBezierSplineTM tm) {
            TCBezierSplineModel model;
            model.start = tm.start;
            model.end = tm.end;
            model.c1 = tm.c1;
            model.c2 = tm.c2;
            model.duration = tm.duration;
            model.timeEasingType = tm.timeEasingType;
            return model;
        }

        public static TCBezierSplineModel[] ToTCBezierSplineModelArray(TCBezierSplineTM[] tmArray) {
            TCBezierSplineModel[] modelArray = new TCBezierSplineModel[tmArray.Length];
            for (int i = 0; i < tmArray.Length; i++) {
                modelArray[i] = ToTCBezierSplineTM(tmArray[i]);
            }
            return modelArray;
        }

        public static TCFollowModel ToTCFollowModel(in TCFollowTM tm) {
            TCFollowModel model;
            model.followType = tm.followType;
            model.normalFollowOffset = tm.normalFollowOffset;
            model.easingType_horizontal = tm.easingType_horizontal;
            model.easingType_vertical = tm.easingType_vertical;
            model.duration_horizontal = tm.duration_horizontal;
            model.duration_vertical = tm.duration_vertical;
            return model;
        }

        public static TCLookAtModel ToTCLookAtModel(in TCLookAtTM tm) {
            TCLookAtModel model;
            model.easingType_horizontal = tm.easingType_horizontal;
            model.easingType_vertical = tm.easingType_vertical;
            model.duration_horizontal = tm.duration_horizontal;
            model.duration_vertical = tm.duration_vertical;
            model.normalLookActivated = tm.normalLookActivated;
            model.normalLookAngles = tm.normalLookAngles;
            model.lookAtTargetOffset = tm.lookAtTargetOffset;

            model.maxLookDownDegree = tm.maxLookDownDegree;
            model.maxLookUpDegree = tm.maxLookUpDegree;
            model.composerModel = ToTCLookAtComposerModel(tm.composerTM);
            return model;
        }

        public static TCLookAtComposerModel ToTCLookAtComposerModel(in TCLookAtComposerTM tm) {
            TCLookAtComposerModel model;
            model.composerType = tm.composerType;
            model.screenNormalizedX = tm.screenNormalizedX;
            model.screenNormalizedY = tm.screenNormalizedY;
            model.deadZoneNormalizedW = tm.deadZoneNormalizedW;
            model.deadZoneNormalizedH = tm.deadZoneNormalizedH;
            model.softZoneNormalizedW = tm.softZoneNormalizedW;
            model.softZoneNormalizedH = tm.softZoneNormalizedH;
            model.normalDamping = tm.normalDamping;
            model.normalLookAngles = tm.normalLookAngles;
            model.normalLookActivated = tm.normalLookActivated;
            return model;
        }

        public static TCFOVStateModel ToTCFOVModel(in TCFOVStateTM tm) {
            TCFOVStateModel model;
            model.offset = tm.offset;
            model.duration = tm.duration;
            model.easingType = tm.easingType;
            model.isInherit = tm.isInherit;
            return model;
        }

        public static TCPushStateModel ToTCPushModel(in TCPushStateTM tm) {
            TCPushStateModel model;
            model.offset = tm.offset;
            model.duration = tm.duration;
            model.easingType = tm.easingType;
            model.isInherit = tm.isInherit;
            return model;
        }

        public static TCRotateStateModel ToTCRotateModel(in TCRotateStateTM tm) {
            TCRotateStateModel model;
            model.offset = tm.offset;
            model.duration = tm.duration;
            model.easingType = tm.easingType;
            model.isInherit = tm.isInherit;
            return model;
        }


    }

}