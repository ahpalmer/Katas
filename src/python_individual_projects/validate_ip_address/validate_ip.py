import ipaddress
import sys

def validate_ip(ip):
    try:
        ip = ipaddress.ip_address(ip)
        return True
    except ValueError:
        return False
    except:
        return False