import os
import shutil

def remove_bin(root_dir='.'):
    for root, dirs, files in os.walk(root_dir):
        if 'bin' in dirs:
            bin_path = os.path.join(root, 'bin')
            print(f"Deleting: {bin_path}")
            shutil.rmtree(bin_path, ignore_errors=True)


def remove_obj(root_dir='.'):
    for root, dirs, files in os.walk(root_dir):
        if 'obj' in dirs:
            obj_path = os.path.join(root, 'obj')
            print(f"Deleting: {obj_path}")
            shutil.rmtree(obj_path, ignore_errors=True)
  
  
def remove_publish_profiles(root_dir='.'):
    for root, dirs, files in os.walk(root_dir):
        if 'PublishProfiles' in dirs:
            publish_profiles_path = os.path.join(root, 'PublishProfiles')
            print(f"Deleting: {publish_profiles_path}")
            shutil.rmtree(publish_profiles_path, ignore_errors=True)


if __name__ == "__main__":
    remove_bin()                # 默认清理当前目录，可指定其他路径如 `remove_bin('/path/to/project')`
    remove_obj()                # 默认清理当前目录，可指定其他路径如 `remove_obj('/path/to/project')`
    remove_publish_profiles()   # 默认清理当前目录，可指定其他路径如 `remove_publish_profiles('/path/to/project')`
