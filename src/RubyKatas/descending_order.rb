def descending_order(n)
  digits = n.digits.reverse
  sorted = digits.sort
  my_int = 0
  counter = 1
  sorted.each do |digit|
    my_int = my_int + digit * counter
    counter *= 10
  end
  puts my_int
  return my_int
end

def descending_order_better(n)
  n.to_s.chars.sort.reverse.join.to_i
end

descending_order(3738)