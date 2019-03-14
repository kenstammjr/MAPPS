using System;
using System.Security.Principal;

namespace MAPPS {
    public class Impersonator : IDisposable {

        public Impersonator() {
            UseAppPoolIdentity();
        }

        public void Dispose() {
            UndoImpersonation();
        }

        private void UseAppPoolIdentity() {
            try {
                if (!WindowsIdentity.GetCurrent().IsSystem) {
                    impersonationContext = WindowsIdentity.Impersonate(IntPtr.Zero);
                }
            } catch { }
        }

        private void UndoImpersonation() {
            if (impersonationContext != null) {
                impersonationContext.Undo();
            }
        }

        private WindowsImpersonationContext impersonationContext = null;
    }
}
