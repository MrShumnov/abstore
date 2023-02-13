with open('nginx/logs/access_balance.log', 'r') as f:
    s = f.read()

print(f'5000: { s.count("5000") }',
    f'5002: { s.count("5002") }',
    f'5003: { s.count("5003") }', sep='\n')