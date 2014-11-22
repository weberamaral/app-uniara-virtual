using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace UniaraVirtual.Assets.Helpers
{
    /// <summary>
    /// Behavor customizado que atualiza os dados em um Passwordbox assim que o texto é alterado no controle
    /// </summary>
    public class UpdatePasswordBindingOnPropertyChanged : Behavior<PasswordBox>
    {
        private BindingExpression expression;

        /// <summary>
        /// Chamado depois que o behavior é anexado no AssociateObject
        /// </summary>
        /// <remarks>
        /// Sobrescreve a funcionalidade do AssociateObject
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.expression = this.AssociatedObject.GetBindingExpression(PasswordBox.PasswordProperty);
            this.AssociatedObject.PasswordChanged += this.OnPasswordChanged;
        }

        /// <summary>
        /// Chamando quando o controle irá retirrar do AssociateObject, mas antes de ocorrer
        /// </summary>
        /// <remarks>
        /// Sobreecreve a funcionalidade de AssociateObject
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.PasswordChanged -= this.OnPasswordChanged;
            this.expression = null;
        }

        private void OnPasswordChanged(object sender, EventArgs args)
        {
            this.expression.UpdateSource();
        }
    }
}
