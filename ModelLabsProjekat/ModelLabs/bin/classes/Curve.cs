//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FTN {
    using System;
    using FTN;
    
    
    /// A multi-purpose curve or functional relationship between an independent variable (X-axis) and dependent (Y-axis) variables.
    public class Curve : IdentifiedObject {
        
        /// The style or shape of the curve.
        private CurveStyle? cim_curveStyle;
        
        private const bool isCurveStyleMandatory = false;
        
        private const string _curveStylePrefix = "cim";
        
        /// Multiplier for X-axis.
        private UnitMultiplier? cim_xMultiplier;
        
        private const bool isXMultiplierMandatory = false;
        
        private const string _xMultiplierPrefix = "cim";
        
        /// The X-axis units of measure.
        private UnitSymbol? cim_xUnit;
        
        private const bool isXUnitMandatory = false;
        
        private const string _xUnitPrefix = "cim";
        
        /// Multiplier for Y1-axis.
        private UnitMultiplier? cim_y1Multiplier;
        
        private const bool isY1MultiplierMandatory = false;
        
        private const string _y1MultiplierPrefix = "cim";
        
        /// The Y1-axis units of measure.
        private UnitSymbol? cim_y1Unit;
        
        private const bool isY1UnitMandatory = false;
        
        private const string _y1UnitPrefix = "cim";
        
        /// Multiplier for Y2-axis.
        private UnitMultiplier? cim_y2Multiplier;
        
        private const bool isY2MultiplierMandatory = false;
        
        private const string _y2MultiplierPrefix = "cim";
        
        /// The Y2-axis units of measure.
        private UnitSymbol? cim_y2Unit;
        
        private const bool isY2UnitMandatory = false;
        
        private const string _y2UnitPrefix = "cim";
        
        /// Multiplier for Y3-axis.
        private UnitMultiplier? cim_y3Multiplier;
        
        private const bool isY3MultiplierMandatory = false;
        
        private const string _y3MultiplierPrefix = "cim";
        
        /// The Y3-axis units of measure.
        private UnitSymbol? cim_y3Unit;
        
        private const bool isY3UnitMandatory = false;
        
        private const string _y3UnitPrefix = "cim";
        
        public virtual CurveStyle CurveStyle {
            get {
                return this.cim_curveStyle.GetValueOrDefault();
            }
            set {
                this.cim_curveStyle = value;
            }
        }
        
        public virtual bool CurveStyleHasValue {
            get {
                return this.cim_curveStyle != null;
            }
        }
        
        public static bool IsCurveStyleMandatory {
            get {
                return isCurveStyleMandatory;
            }
        }
        
        public static string CurveStylePrefix {
            get {
                return _curveStylePrefix;
            }
        }
        
        public virtual UnitMultiplier XMultiplier {
            get {
                return this.cim_xMultiplier.GetValueOrDefault();
            }
            set {
                this.cim_xMultiplier = value;
            }
        }
        
        public virtual bool XMultiplierHasValue {
            get {
                return this.cim_xMultiplier != null;
            }
        }
        
        public static bool IsXMultiplierMandatory {
            get {
                return isXMultiplierMandatory;
            }
        }
        
        public static string XMultiplierPrefix {
            get {
                return _xMultiplierPrefix;
            }
        }
        
        public virtual UnitSymbol XUnit {
            get {
                return this.cim_xUnit.GetValueOrDefault();
            }
            set {
                this.cim_xUnit = value;
            }
        }
        
        public virtual bool XUnitHasValue {
            get {
                return this.cim_xUnit != null;
            }
        }
        
        public static bool IsXUnitMandatory {
            get {
                return isXUnitMandatory;
            }
        }
        
        public static string XUnitPrefix {
            get {
                return _xUnitPrefix;
            }
        }
        
        public virtual UnitMultiplier Y1Multiplier {
            get {
                return this.cim_y1Multiplier.GetValueOrDefault();
            }
            set {
                this.cim_y1Multiplier = value;
            }
        }
        
        public virtual bool Y1MultiplierHasValue {
            get {
                return this.cim_y1Multiplier != null;
            }
        }
        
        public static bool IsY1MultiplierMandatory {
            get {
                return isY1MultiplierMandatory;
            }
        }
        
        public static string Y1MultiplierPrefix {
            get {
                return _y1MultiplierPrefix;
            }
        }
        
        public virtual UnitSymbol Y1Unit {
            get {
                return this.cim_y1Unit.GetValueOrDefault();
            }
            set {
                this.cim_y1Unit = value;
            }
        }
        
        public virtual bool Y1UnitHasValue {
            get {
                return this.cim_y1Unit != null;
            }
        }
        
        public static bool IsY1UnitMandatory {
            get {
                return isY1UnitMandatory;
            }
        }
        
        public static string Y1UnitPrefix {
            get {
                return _y1UnitPrefix;
            }
        }
        
        public virtual UnitMultiplier Y2Multiplier {
            get {
                return this.cim_y2Multiplier.GetValueOrDefault();
            }
            set {
                this.cim_y2Multiplier = value;
            }
        }
        
        public virtual bool Y2MultiplierHasValue {
            get {
                return this.cim_y2Multiplier != null;
            }
        }
        
        public static bool IsY2MultiplierMandatory {
            get {
                return isY2MultiplierMandatory;
            }
        }
        
        public static string Y2MultiplierPrefix {
            get {
                return _y2MultiplierPrefix;
            }
        }
        
        public virtual UnitSymbol Y2Unit {
            get {
                return this.cim_y2Unit.GetValueOrDefault();
            }
            set {
                this.cim_y2Unit = value;
            }
        }
        
        public virtual bool Y2UnitHasValue {
            get {
                return this.cim_y2Unit != null;
            }
        }
        
        public static bool IsY2UnitMandatory {
            get {
                return isY2UnitMandatory;
            }
        }
        
        public static string Y2UnitPrefix {
            get {
                return _y2UnitPrefix;
            }
        }
        
        public virtual UnitMultiplier Y3Multiplier {
            get {
                return this.cim_y3Multiplier.GetValueOrDefault();
            }
            set {
                this.cim_y3Multiplier = value;
            }
        }
        
        public virtual bool Y3MultiplierHasValue {
            get {
                return this.cim_y3Multiplier != null;
            }
        }
        
        public static bool IsY3MultiplierMandatory {
            get {
                return isY3MultiplierMandatory;
            }
        }
        
        public static string Y3MultiplierPrefix {
            get {
                return _y3MultiplierPrefix;
            }
        }
        
        public virtual UnitSymbol Y3Unit {
            get {
                return this.cim_y3Unit.GetValueOrDefault();
            }
            set {
                this.cim_y3Unit = value;
            }
        }
        
        public virtual bool Y3UnitHasValue {
            get {
                return this.cim_y3Unit != null;
            }
        }
        
        public static bool IsY3UnitMandatory {
            get {
                return isY3UnitMandatory;
            }
        }
        
        public static string Y3UnitPrefix {
            get {
                return _y3UnitPrefix;
            }
        }
    }
}
