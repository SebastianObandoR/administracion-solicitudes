
function BrandLogo({ size = "md" }) {
  return (
    <div className={`brand-logo brand-logo-${size}`}>
      <i className="bi bi-building-check"></i>
    </div>
  );
}

export default BrandLogo;
