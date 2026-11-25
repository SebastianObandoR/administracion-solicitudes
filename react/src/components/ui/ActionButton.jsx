
function ActionButton({
  icon,
  children,
  onClick,
  type = "button",
  variant = "primary",
  round = false,
  disabled = false,
  title = ""
}) {
  return (
    <button
      type={type}
      className={`crud-button crud-button-${variant} ${round ? "crud-button-round" : ""}`}
      onClick={onClick}
      disabled={disabled}
      title={title}
      aria-label={title || (typeof children === "string" ? children : undefined)}
    >
      {icon && <i className={`bi ${icon}`}></i>}
      {children && <span>{children}</span>}
    </button>
  );
}

export default ActionButton;
