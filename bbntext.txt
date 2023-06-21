import random

input_ports = {}


class Packet:

    # init yöntemi veya yapıcıinit yöntemi veya yapıcı
    def __init__(self, data: int):
        self.data = data # 0
        self.bin = "{0:04b}".format(data) # 0000
        self.path = [] # sütun adlarının listesi # [a1,b1,c1,d1,outpu0]

    # Örnek Yöntem
  
    def __str__(self):
        return f'Data = {self.data}, Binary = {self.bin},\nPath = {"->".join(self.path)}'


def batcher_sorter(list_a: list) -> list:
    list_a = sorted(list_a)
    list_b = []  # paket listesi
    for i in list_a:
        packet = Packet(i)
        list_b.append(packet)
    initialize_input_ports()
    return list_b


def initialize_input_ports():
    global input_ports
    input_ports = {
        0: 'A1',
        1: 'A2',
        2: 'A3',
        3: 'A4',
        4: 'A5',
        5: 'A6',
        6: 'A7',
        7: 'A8',
        8: 'A1',
        9: 'A2',
        10: 'A3',
        11: 'A4',
        12: 'A5',
        13: 'A6',
        14: 'A7',
        15: 'A8',
    }


def initialize_switches():
    switch_ab = {
        'A1': ['B1', 'B5'],
        'A2': ['B2', 'B6'],
        'A3': ['B3', 'B7'],
        'A4': ['B4', 'B8'],
        'A5': ['B1', 'B5'],
        'A6': ['B2', 'B6'],
        'A7': ['B3', 'B7'],
        'A8': ['B4', 'B8']
    }

    switch_bc = {
        'B1': ['C1', 'C3'],
        'B2': ['C2', 'C4'],
        'B3': ['C1', 'C3'],
        'B4': ['C2', 'C4'],
        'B5': ['C5', 'C7'],
        'B6': ['C6', 'C8'],
        'B7': ['C5', 'C7'],
        'B8': ['C6', 'C8']
    }

    switch_cd = {
        'C1': ['D1', 'D2'],
        'C2': ['D1', 'D2'],
        'C3': ['D3', 'D4'],
        'C4': ['D3', 'D4'],
        'C5': ['D5', 'D6'],
        'C6': ['D5', 'D6'],
        'C7': ['D7', 'D8'],
        'C8': ['D7', 'D8']
    }

    switch_d_out = {
        'D1': ['CIKIS 0', 'CIKIS 1'],
        'D2': ['CIKIS 2', 'CIKIS 3'],
        'D3': ['CIKIS 4', 'CIKIS 5'],
        'D4': ['CIKIS 6', 'CIKIS 7'],
        'D5': ['CIKIS 8', 'CIKIS 9'],
        'D6': ['CIKIS 10', 'CIKIS 11'],
        'D7': ['CIKIS 12', 'CIKIS 13'],
        'D8': ['CIKIS 14', 'CIKIS 15']
    }
    return switch_ab, switch_bc, switch_cd, switch_d_out


def find_path(packet):
    s1, s2, s3, s4 = initialize_switches()

    #  paketin A sütununda nereye gireceğini bul
    packet.path.append(input_ports.get(packet.data))
    # packet.data = 3 ==> 0011
    # ['A1']

    #  paketin A'dan B'ye nereye gideceğini bul
    packet.path.append(s1.get(packet.path[0])[0] if packet.bin[0] == '0' else s1.get(packet.path[0])[1])
    # ['A1', 'B1']

    #  paketin B'den C'ye nereye gideceğini bul
    # print(s2.get(packet.path[1])[0] if packet.bin[1] == '0')
    packet.path.append(s2.get(packet.path[1])[0] if packet.bin[1] == '0' else s2.get(packet.path[1])[1])

    # paketin C'den D'ye nereye gideceğini bul
    packet.path.append(s3.get(packet.path[2])[0] if packet.bin[2] == '0' else s3.get(packet.path[2])[1])
    # ['A1', 'B1','C1', 'D2']

    # paketin D'den çıktıya nereye gideceğini alın
    packet.path.append(s4.get(packet.path[3])[0] if packet.bin[3] == '0' else s4.get(packet.path[3])[1])
    # ['A1', 'B1','C1', 'D2', 'CIKIS 3']

def banyan_network(list_of_packets: list):
    for packet in list_of_packets:
        # print(f'Data = {packet.data} & Binary = {packet.bin}')
        find_path(packet)
        # print(" --> ".join(packet.path))
        # print()
    pass


def main():
    mxm = 16

    input_sequence = random.sample(range(0, mxm), 15)
    print(f'Input Sequence = {input_sequence}')
    print()

    # batch Uygulama
    print("Batcher Network")
    sorted_input_sequence = batcher_sorter(input_sequence) # list of packets
    print(f'Sorted Input = {[x.data for x in sorted_input_sequence]}')
    print()

    #  banyan Uygulama
    print("Banyan Network")
    banyan_network(sorted_input_sequence)
    for packet in sorted_input_sequence:
        print(str(packet.data).rjust(2) + "  -->  " + "  -->  ".join(packet.path))
        # print(packet.data, packet.path)

if __name__ == '__main__':
    main()