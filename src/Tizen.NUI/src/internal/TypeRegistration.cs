//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.9
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Tizen.NUI {

public class TypeRegistration : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal TypeRegistration(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(TypeRegistration obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~TypeRegistration() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NDalicPINVOKE.delete_TypeRegistration(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  static private global::System.IntPtr SwigConstructTypeRegistration(SWIGTYPE_p_std__type_info registerType, SWIGTYPE_p_std__type_info baseType, System.Delegate f) {
System.IntPtr ip = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(f); 
    return NDalicPINVOKE.new_TypeRegistration__SWIG_0(SWIGTYPE_p_std__type_info.getCPtr(registerType), SWIGTYPE_p_std__type_info.getCPtr(baseType), new System.Runtime.InteropServices.HandleRef(null, ip));
  }

  public TypeRegistration(SWIGTYPE_p_std__type_info registerType, SWIGTYPE_p_std__type_info baseType, System.Delegate f) : this(TypeRegistration.SwigConstructTypeRegistration(registerType, baseType, f), true) {
    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
  }

  static private global::System.IntPtr SwigConstructTypeRegistration(SWIGTYPE_p_std__type_info registerType, SWIGTYPE_p_std__type_info baseType, System.Delegate f, bool callCreateOnInit) {
System.IntPtr ip = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(f); 
    return NDalicPINVOKE.new_TypeRegistration__SWIG_1(SWIGTYPE_p_std__type_info.getCPtr(registerType), SWIGTYPE_p_std__type_info.getCPtr(baseType), new System.Runtime.InteropServices.HandleRef(null, ip), callCreateOnInit);
  }

  public TypeRegistration(SWIGTYPE_p_std__type_info registerType, SWIGTYPE_p_std__type_info baseType, System.Delegate f, bool callCreateOnInit) : this(TypeRegistration.SwigConstructTypeRegistration(registerType, baseType, f, callCreateOnInit), true) {
    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
  }

  static private global::System.IntPtr SwigConstructTypeRegistration(string name, SWIGTYPE_p_std__type_info baseType, System.Delegate f) {
System.IntPtr ip = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(f); 
    return NDalicPINVOKE.new_TypeRegistration__SWIG_2(name, SWIGTYPE_p_std__type_info.getCPtr(baseType), new System.Runtime.InteropServices.HandleRef(null, ip));
  }

  public TypeRegistration(string name, SWIGTYPE_p_std__type_info baseType, System.Delegate f) : this(TypeRegistration.SwigConstructTypeRegistration(name, baseType, f), true) {
    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
  }

  public string RegisteredName() {
    string ret = NDalicPINVOKE.TypeRegistration_RegisteredName(swigCPtr);
    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void RegisterControl(string controlName, System.Delegate createFunc) {
System.IntPtr ip = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(createFunc); 
    {
      NDalicPINVOKE.TypeRegistration_RegisterControl(controlName, new System.Runtime.InteropServices.HandleRef(null, ip));
      if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
    }
  }

  public static void RegisterProperty(string controlName, string propertyName, int index, PropertyType type, System.Delegate setFunc, System.Delegate getFunc) {
System.IntPtr ip = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(setFunc); 
System.IntPtr ip2 = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(getFunc); 
    {
      NDalicPINVOKE.TypeRegistration_RegisterProperty(controlName, propertyName, index, (int)type, new System.Runtime.InteropServices.HandleRef(null, ip), new System.Runtime.InteropServices.HandleRef(null, ip2));
      if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
    }
  }

}

}
