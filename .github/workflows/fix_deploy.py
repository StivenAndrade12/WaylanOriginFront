import os
import glob
import shutil

wwwroot = "output/wwwroot"
framework = os.path.join(wwwroot, "_framework")

# 1. Alias fingerprinted files in _framework
if os.path.exists(framework):
    for fname in os.listdir(framework):
        full_path = os.path.join(framework, fname)
        if os.path.isdir(full_path):
            continue
        parts = fname.split('.')
        if len(parts) == 4 and parts[0] in ['blazor', 'dotnet']:
            alias_name = f"{parts[0]}.{parts[1]}.{parts[3]}"
            alias_path = os.path.join(framework, alias_name)
            if not os.path.exists(alias_path):
                shutil.copyfile(full_path, alias_path)
                print(f"Aliased {fname} -> {alias_name}")
        elif len(parts) == 3 and parts[0] == 'dotnet' and parts[2] == 'js':
            alias_name = "dotnet.js"
            alias_path = os.path.join(framework, alias_name)
            if not os.path.exists(alias_path):
                shutil.copyfile(full_path, alias_path)
                print(f"Aliased {fname} -> {alias_name}")

# 2. Fix base href and script tag in index.html
index_path = os.path.join(wwwroot, "index.html")
if os.path.exists(index_path):
    with open(index_path, "r", encoding="utf-8") as f:
        html = f.read()
    
    if os.path.exists(framework):
        for fname in os.listdir(framework):
            if fname.startswith("blazor.webassembly.") and fname.endswith(".js"):
                html = html.replace("_framework/blazor.webassembly.js", f"_framework/{fname}")
                print(f"Updated index.html script tag to _framework/{fname}")
                break

    html = html.replace('<base href="/" />', '<base href="/WaylanOriginFront/" />')
    with open(index_path, "w", encoding="utf-8") as f:
        f.write(html)
    print("Updated index.html base href.")

    # 3. Create 404.html for SPA routing
    shutil.copyfile(index_path, os.path.join(wwwroot, "404.html"))
    print("Created 404.html")

# 4. Touch .nojekyll
with open(os.path.join(wwwroot, ".nojekyll"), "w") as f:
    f.write("# Disable Jekyll\n")
print("Created .nojekyll")
