// node_modules/@angular/core/fesm2022/not_found.mjs
var _currentInjector = void 0;
function getCurrentInjector() {
  return _currentInjector;
}
function setCurrentInjector(injector) {
  const former = _currentInjector;
  _currentInjector = injector;
  return former;
}
function inject(token, options) {
  const currentInjector = getCurrentInjector();
  if (!currentInjector) {
    throw new Error("Current injector is not set.");
  }
  if (!token.ɵprov) {
    throw new Error("Token is not an injectable");
  }
  return currentInjector.retrieve(token, options);
}
var NOT_FOUND = Symbol("NotFound");
var NotFoundError = class extends Error {
  name = "ɵNotFound";
  constructor(message) {
    super(message);
  }
};
function isNotFound(e) {
  return e === NOT_FOUND || e?.name === "ɵNotFound";
}

// node_modules/@angular/core/fesm2022/primitives/di.mjs
function defineInjectable(opts) {
  return {
    token: opts.token,
    providedIn: opts.providedIn || null,
    factory: opts.factory,
    value: void 0
  };
}
function registerInjectable(ctor, declaration) {
  ctor.ɵprov = declaration;
  return ctor;
}

export {
  getCurrentInjector,
  setCurrentInjector,
  inject,
  NOT_FOUND,
  NotFoundError,
  isNotFound,
  defineInjectable,
  registerInjectable
};
/*! Bundled license information:

@angular/core/fesm2022/not_found.mjs:
@angular/core/fesm2022/primitives/di.mjs:
  (**
   * @license Angular v20.2.4
   * (c) 2010-2025 Google LLC. https://angular.io/
   * License: MIT
   *)
*/
//# sourceMappingURL=chunk-5CC4ERJ4.js.map
