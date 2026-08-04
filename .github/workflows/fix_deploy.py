import os
import glob
import shutil

wwwroot = "output/wwwroot"
old_fw = os.path.join(wwwroot, "_framework")
new_fw = os.path.join(wwwroot, "framework")

# 1. Copy _framework to framework so URLs under framework/ also work without Jekyll blocking
if os.path.exists(old_fw):
    if os.path.exists(new_fw):
        shutil.rmtree(new_fw)
    shutil.copytree(old_fw, new_fw)
    print("Copied _framework to framework")

# 2. Process both _framework and framework directories for aliasing and references
for target_dir in [old_fw, new_fw]:
    if not os.path.exists(target_dir):
        continue
    for fname in list(os.listdir(target_dir)):
        full_path = os.path.join(target_dir, fname)
        if os.path.isdir(full_path):
            continue
        
        # Replace _framework with framework in JS files if in framework directory
        if target_dir == new_fw and fname.endswith(".js"):
            try:
                with open(full_path, "r", encoding="utf-8") as f:
                    content = f.read()
                if "_framework" in content:
                    content = content.replace("_framework", "framework")
                    with open(full_path, "w", encoding="utf-8") as f:
                        f.write(content)
                    print(f"Replaced _framework in {fname}")
            except Exception as e:
                print(f"Could not text-process {fname}: {e}")

        # Handle fingerprinted files
        parts = fname.split('.')
        if len(parts) == 4 and parts[0] in ['blazor', 'dotnet']:
            alias_name = f"{parts[0]}.{parts[1]}.{parts[3]}"
            alias_path = os.path.join(target_dir, alias_name)
            if not os.path.exists(alias_path):
                shutil.copyfile(full_path, alias_path)
                print(f"Aliased {fname} -> {alias_name}")
        elif len(parts) == 3 and parts[0] == 'dotnet' and parts[2] == 'js':
            alias_name = "dotnet.js"
            alias_path = os.path.join(target_dir, alias_name)
            if not os.path.exists(alias_path):
                shutil.copyfile(full_path, alias_path)
                print(f"Aliased {fname} -> {alias_name}")

# 3. Update index.html
index_path = os.path.join(wwwroot, "index.html")
if os.path.exists(index_path):
    with open(index_path, "r", encoding="utf-8") as f:
        html = f.read()
    
    html = html.replace('_framework/', 'framework/')
    html = html.replace('<base href="/" />', '<base href="/WaylanOriginFront/" />')
    
    with open(index_path, "w", encoding="utf-8") as f:
        f.write(html)
    print("Updated index.html base href and framework path.")

    # 4. Create 404.html for SPA routing
    shutil.copyfile(index_path, os.path.join(wwwroot, "404.html"))
    print("Created 404.html")

# 5. Touch .nojekyll in wwwroot
with open(os.path.join(wwwroot, ".nojekyll"), "w") as f:
    f.write("# Disable Jekyll\n")
print("Created .nojekyll")
