mergeInto(LibraryManager.library, {
  Login: function () {
    try {
      dispatchReactUnityEvent("Login");
    }catch(e) {

    }
   
  },
  Logout: function () {
    try {
      dispatchReactUnityEvent("Logout");
    }catch(e) {

    }
  },
  Refill: function () {
    try {
      dispatchReactUnityEvent("Refill");
    }catch(e) {

    }
  }
});
