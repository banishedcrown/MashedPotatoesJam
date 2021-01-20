mergeInto(LibraryManager.library, {

  syncSystem: function () {
    FS.syncfs(false, function (err) {
    });
  },
});