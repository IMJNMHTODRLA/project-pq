import secrets

value = secrets.randbits(64)
print(f"0x{value:016X}")